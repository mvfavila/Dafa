namespace DAFA.Infra.Data.Migrations
{
    using DAFA.Domain.Entities;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Context.DAFAContext>
    {
        public Configuration()
        {
            // TODO: set to false before launch
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Context.DAFAContext context)
        {
            //  This method will be called after migrating to the latest version.

            #region Client

            var client01 = Client.FactorySeed("d6e09d11-1cae-44f3-b834-99741f8ed781", "Marcos Vinicius S.A.", "mvfavila@gmail.com", "(79) 3249-1234", true);

            context.Clients.AddOrUpdate(
                c => c.ClientId,
                client01
                );

            #endregion

            #region Field

            var field01 = Field.FactorySeed("af0ed9b6-eebe-4f15-84b5-b3241ce33eae", "Jazida de Itabaiana", client01.ClientId, true);

            context.Fields.AddOrUpdate(
                f => f.FieldId,
                field01
                );

            #endregion

            #region EventType

            var eventType30Dias = EventType.FactorySeed("7a1a3567-9cd5-41ff-8f17-e76bdc207f69", "30 dias para aviso", 30, true);
            var eventType60Dias = EventType.FactorySeed("6f21d3b8-3de2-416a-b654-b25e1e6cb7f9", "60 dias para aviso", 60, true);

            context.EventTypes.AddOrUpdate(
                e => e.EventTypeId,
                eventType30Dias,
                eventType60Dias
                );

            #endregion

            #region Event

            var eventDNPM = Event.FactorySeed("76066e45-10f1-4368-9fb9-0e1511b8479b", "DNPM", "Licença da DNPM", DateTime.Now, field01.FieldId, eventType30Dias.EventTypeId);
            var eventAdema = Event.FactorySeed("dded68e1-7a21-4d30-bc7c-ce8ab3b2ea96", "Adema", "Licença da Adema", DateTime.Now, field01.FieldId, eventType60Dias.EventTypeId);

            context.Events.AddOrUpdate(
                e => e.EventTypeId,
                eventDNPM,
                eventAdema
                );

            #endregion

            #region Navigation - Menu

            var menuItemHome = MenuItem.FactorySeed("e74d11b7-ae56-4524-b036-a953d0c7eee9", "Home", "Index", "Home", null, null, null);
            var menuItemClient = MenuItem.FactorySeed("273f39b2-2c91-450e-9f91-333818b932b9", "Cliente", "Index", "Client", null, "ViewClient", "True");
            var menuItemField = MenuItem.FactorySeed("fafcbb03-0117-4db5-95a8-a792ae20fbe8", "Jazida", "Index", "Field", null, "ViewField", "True");
            var menuItemEvent = MenuItem.FactorySeed("4a7a335f-aae1-45e0-a488-2911c518e939", "Evento", "Index", "Event", null, "ViewEvent", "True");
            var menuItemEventType = MenuItem.FactorySeed("5fc0aee8-8ef5-4c1a-bb02-51e7af4f104b", "Tipo de Evento", "Index", "EventType", null, "ViewEventType", "True");
            var menuItemPeriodicity = MenuItem.FactorySeed("c066f85f-8527-42b6-b3e9-3015eb2d2ea3", "Peridiocidade", "Index", "Periodicity", null, "ViewPeriodicity", "True");

            context.MenuItems.AddOrUpdate(
                m => m.Name,
                menuItemHome,
                menuItemClient,
                menuItemField,
                menuItemEvent,
                menuItemEventType,
                menuItemPeriodicity
                );

            #endregion
        }
    }
}
