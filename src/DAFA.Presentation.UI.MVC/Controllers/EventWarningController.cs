using DAFA.Application.Interfaces;
using DAFA.Application.ViewModels;
using DAFA.Infra.CrossCutting.Identity.Configuration;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace DAFA.Presentation.UI.MVC.Controllers
{
    public class EventWarningController : Controller
    {
        private const string DATE_FORMAT = "dd/MM/yyyy";
        private readonly IEventAppService eventAppService;
        private readonly IEventWarningAppService eventWarningAppService;

        public EventWarningController(IEventAppService eventAppService, IEventWarningAppService eventWarningAppService)
        {
            this.eventAppService = eventAppService;
            this.eventWarningAppService = eventWarningAppService;
        }

        public void ProcessEventWarnings()
        {
            var overdueEvents = eventAppService.ProcessEventWarnings();

            if (overdueEvents.Any())
                AddEventWarnings(overdueEvents);

            var eventWarnings = eventWarningAppService.GetUnsolved();

            var emailBody = GetEmailBody(eventWarnings);

            SendEventWarnings(emailBody);
        }

        private static void SendEventWarnings(StringBuilder emailBody)
        {
            using (var emailService = new EmailService())
            {
                emailService.SendAsync(new IdentityMessage
                {
                    Destination = "mvfavila@gmail.com",
                    Subject = $"DAFA - Alertas {DateTime.Today.ToString(DATE_FORMAT)}",
                    Body = emailBody.ToString()
                });
            }
        }

        private static StringBuilder GetEmailBody(IEnumerable<EventWarningViewModel> eventWarnings)
        {
            var builder = new StringBuilder();
            foreach (var ew in eventWarnings)
            {
                var line = $@"Jazida '{ew.Field.Name}'.
                    O evento '{ew.Event.Name}' vence no dia {ew.Date.ToString(DATE_FORMAT)}";
                builder.Append(line).Append("<br/>");
            }

            return builder;
        }

        private void AddEventWarnings(IEnumerable<EventViewModel> overdueEvents)
        {
            foreach (var e in overdueEvents)
            {
                var viewModel = new EventWarningViewModel
                {
                    EventId = e.EventId,
                    Event = e
                };

                eventWarningAppService.Add(viewModel);
            }
        }
    }
}