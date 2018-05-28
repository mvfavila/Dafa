namespace DAFA.Infra.CrossCutting.Identity.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Context.AppDbContext>
    {
        public Configuration()
        {
            // TODO: set to false before launch
            AutomaticMigrationsEnabled = true;
            ContextKey = "DAFA.Infra.CrossCutting.Identity.Context.AppDbContext";
        }

        protected override void Seed(Context.AppDbContext context)
        {
            var claimViewClient = Claims.FactorySeed("dcf9f181-3bec-4acc-9776-a5ca9a7836c8", "ViewClient");
            var claimCreateClient = Claims.FactorySeed("21d37233-6c45-4a50-9a05-c456db3f24db", "CreateClient");
            var claimEditClient = Claims.FactorySeed("dca9a27b-a899-4a1b-bf4d-43083e19b25e", "EditClient");
            var claimDeactivateClient = Claims.FactorySeed("bedcee8d-b303-492e-bfe8-59850f70a83a", "DeactivateClient");
            var claimViewEvent = Claims.FactorySeed("3320fb46-2359-4334-aa74-49059f59ad57", "ViewEvent");
            var claimCreateEvent = Claims.FactorySeed("7b83fe6c-5103-41d4-a28f-c364cbf90b1d", "CreateEvent");
            var claimEditEvent = Claims.FactorySeed("67830b59-8780-48f6-b8de-e02039434b23", "EditEvent");
            var claimDeactivateEvent = Claims.FactorySeed("a3193215-3cec-4734-8c86-2bcddde30efa", "DeactivateEvent");
            var claimViewEventType = Claims.FactorySeed("6b97292c-01ea-48a3-90d4-eafa09b33d64", "ViewEventType");
            var claimCreateEventType = Claims.FactorySeed("c3310d7a-a23b-4c98-8d0c-a722e1bfdec8", "CreateEventType");
            var claimEditEventType = Claims.FactorySeed("01aaf3dd-b3bc-47b7-be00-e4501a403813", "EditEventType");
            var claimDeactivateEventType = Claims.FactorySeed("f65a23f8-405a-4ad2-b1a6-e5c63e6ee6db", "DeactivateEventType");
            var claimViewPeriodicity = Claims.FactorySeed("9f5dc01c-7a2b-444a-b967-bc49584516c4", "ViewPeriodicity");
            var claimCreatePeriodicity = Claims.FactorySeed("813c3690-8a88-44d8-a274-78ad8c190a84", "CreatePeriodicity");
            var claimEditPeriodicity = Claims.FactorySeed("94902ff6-db6c-4fe8-ac80-e9972382d624", "EditPeriodicity");
            var claimDeactivatePeriodicity = Claims.FactorySeed("a71c4114-3d06-41ae-9b55-4449567b4b2c", "DeactivatePeriodicity");
            var claimViewField = Claims.FactorySeed("f15ef921-1d61-4bff-817e-69065bd72627", "ViewField");
            var claimCreateField = Claims.FactorySeed("ea36cc2e-ecdd-41d4-a06a-72f67904c16d", "CreateField");
            var claimEditField = Claims.FactorySeed("680805a0-e514-4da3-9904-16e542729b4b", "EditField");
            var claimDeactivateField = Claims.FactorySeed("6e32101e-0d76-437a-aeff-af3fa005ac2b", "DeactivateField");
            var claimManageField = Claims.FactorySeed("7408c9c3-b6f9-4724-84f8-e5c43c51e2a0", "ManageField");

            context.Claims.AddOrUpdate(
                claimViewClient,
                claimCreateClient,
                claimEditClient,
                claimDeactivateClient,
                claimViewEvent,
                claimCreateEvent,
                claimEditEvent,
                claimDeactivateEvent,
                claimViewEventType,
                claimCreateEventType,
                claimEditEventType,
                claimDeactivateEventType,
                claimViewPeriodicity,
                claimCreatePeriodicity,
                claimEditPeriodicity,
                claimDeactivatePeriodicity,
                claimViewField,
                claimCreateField,
                claimEditField,
                claimDeactivateField,
                claimManageField);

            // TODO: remove user addition after development
            var userAdmin = ApplicationUser.FactorySeed("f9babd79-00ca-4b97-83c7-b908f39d5585", "Admin", "Doe", "admin@email.com", @"ACcx4YaAQgp5LxJ75JphPcH6/LcXb/1WlPDWS/OXfIFSxs0tV1Fu9gDKgPnsSU/c8Q==", "b64a9f55-cb3f-4358-b1a9-058ab5676a20");
            userAdmin.Claims.Add(ConvertClaim(1, claimViewEventType, userAdmin, "True"));
            userAdmin.Claims.Add(ConvertClaim(2, claimCreateEventType, userAdmin, "True"));
            userAdmin.Claims.Add(ConvertClaim(3, claimEditEventType, userAdmin, "True"));
            userAdmin.Claims.Add(ConvertClaim(4, claimDeactivateEventType, userAdmin, "True"));
            userAdmin.Claims.Add(ConvertClaim(5, claimViewPeriodicity, userAdmin, "True"));
            userAdmin.Claims.Add(ConvertClaim(6, claimCreatePeriodicity, userAdmin, "True"));
            userAdmin.Claims.Add(ConvertClaim(7, claimEditPeriodicity, userAdmin, "True"));
            userAdmin.Claims.Add(ConvertClaim(8, claimDeactivatePeriodicity, userAdmin, "True"));
            userAdmin.Claims.Add(ConvertClaim(9, claimViewField, userAdmin, "True"));
            userAdmin.Claims.Add(ConvertClaim(10, claimCreateField, userAdmin, "True"));
            userAdmin.Claims.Add(ConvertClaim(11, claimEditField, userAdmin, "True"));
            userAdmin.Claims.Add(ConvertClaim(12, claimDeactivateField, userAdmin, "True"));
            userAdmin.Claims.Add(ConvertClaim(13, claimManageField, userAdmin, "True"));
            userAdmin.Claims.Add(ConvertClaim(14, claimViewEvent, userAdmin, "True"));
            userAdmin.Claims.Add(ConvertClaim(15, claimCreateEvent, userAdmin, "True"));
            userAdmin.Claims.Add(ConvertClaim(16, claimEditEvent, userAdmin, "True"));
            userAdmin.Claims.Add(ConvertClaim(17, claimDeactivateEvent, userAdmin, "True"));
            userAdmin.Claims.Add(ConvertClaim(18, claimViewClient, userAdmin, "True"));
            userAdmin.Claims.Add(ConvertClaim(19, claimCreateClient, userAdmin, "True"));
            userAdmin.Claims.Add(ConvertClaim(20, claimEditClient, userAdmin, "True"));
            userAdmin.Claims.Add(ConvertClaim(21, claimDeactivateClient, userAdmin, "True"));


            context.Users.AddOrUpdate(
                userAdmin
                );
        }

        private static IdentityUserClaim ConvertClaim(int id, Claims claim, ApplicationUser user, string claimValue)
        {
            return new IdentityUserClaim
            {
                Id = id,
                UserId = user.Id,
                ClaimType = claim.Name,
                ClaimValue = claimValue
            };
        }
    }
}
