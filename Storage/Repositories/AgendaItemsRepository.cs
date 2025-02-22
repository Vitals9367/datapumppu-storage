﻿using Dapper;
using Storage.Repositories.Models;
using Storage.Repositories.Providers;
using System.Data;

namespace Storage.Repositories
{
    public interface IAgendaItemsRepository
    {
        Task<List<AgendaItem>> FetchAgendasByMeetingId(string id);

        Task UpsertAgendaItems(List<AgendaItem> agendasItems, IDbConnection connection, IDbTransaction transaction);
    }

    public class AgendaItemsRepository : IAgendaItemsRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        private readonly ILogger<AgendaItemsRepository> _logger;

        public AgendaItemsRepository(IDatabaseConnectionFactory connectionFactory, ILogger<AgendaItemsRepository> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public async Task<List<AgendaItem>> FetchAgendasByMeetingId(string id)
        {
            using var connection = await _connectionFactory.CreateOpenConnection();
            var sqlQuery = @"
                SELECT meeting_id, agenda_point, section, title, case_id_label, html_content Html, html_decision_history DecisionHistoryHtml
                FROM agenda_items ai
                WHERE meeting_id = @id
            ";
            var result = (await connection.QueryAsync<AgendaItem>(sqlQuery, new { @id })).ToList();

            return result;
        }

        public Task UpsertAgendaItems(List<AgendaItem> agendaItems, IDbConnection connection, IDbTransaction transaction)
        {
            _logger.LogInformation("Upserting agenda items");
            var sqlQuery = @"INSERT INTO agenda_items (meeting_id, agenda_point, section, title, case_id_label, html_content, html_decision_history) values(
                @meetingId, 
                @agendaPoint,
                @section,
                @title,
                @caseIdLabel,
                @html,
                @decisionHistoryHtml
            ) ";
            sqlQuery += @"ON CONFLICT (meeting_id, agenda_point, title) DO UPDATE SET 
                section = @section,
                case_id_label = @caseIdLabel,
                html_content = @html,
                html_decision_history = @decisionHistoryHtml
                WHERE agenda_items.meeting_id = @meetingId and agenda_items.agenda_point = @agendaPoint and agenda_items.title = @title
            ;";

            return connection.ExecuteAsync(sqlQuery, agendaItems.Select(item => new
            {
                meetingId = item.MeetingID,
                agendaPoint = item.AgendaPoint,
                section = item.Section,
                title = item.Title,
                caseIdLabel = item.CaseIDLabel,
                html = item.Html,
                decisionHistoryHtml = item.DecisionHistoryHtml
            }), transaction);
        }

    }
}

