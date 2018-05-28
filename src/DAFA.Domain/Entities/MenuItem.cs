using System;

namespace DAFA.Domain.Entities
{
    /// <summary>
    /// Represents a Menu Item.
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        private MenuItem() { }

        /// <summary>
        /// MenuItem Unique Id.
        /// </summary>
        public Guid MenuItemId { get; set; }

        /// <summary>
        /// Menu Item Name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Action to where the Menu Item points.
        /// </summary>
        public string ActionName { get; private set; }

        /// <summary>
        /// Controller to where the Menu Item points.
        /// </summary>
        public string ControllerName { get; private set; }

        /// <summary>
        /// Url to the where the Menu Item points.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Menu Item Claim Type.
        /// e.g. AuthorizeProviderCreation.
        /// </summary>
        public string ClaimType { get; private set; }

        /// <summary>
        /// Menu Item Claim Value.
        /// e.g. True, False.
        /// </summary>
        public string ClaimValue { get; private set; }

        /// <summary>
        /// Factory used when a Menu Item is being loaded from the database context.
        /// </summary>
        /// <param name="name">Menu Item Name.</param>
        /// <param name="actionName">Action to where the Menu Item points.</param>
        /// <param name="controllerName">Controller to where the Menu Item points.</param>
        /// <param name="url">Url to the where the Menu Item points.</param>
        /// <param name="claimType">Menu Item Claim Type.</param>
        /// <param name="claimValue">Menu Item Claim Value.</param>
        /// <returns>See <see cref="MenuItem"/>.</returns>
        public static MenuItem FactoryLoad(string name, string actionName, string controllerName, string url,
            string claimType, string claimValue)
        {
            return new MenuItem
            {
                Name = name,
                ActionName = actionName,
                ControllerName = controllerName,
                Url = url,
                ClaimType = claimType,
                ClaimValue = claimValue
            };
        }

        /// <summary>
        /// Factory used to seed the database context.
        /// </summary>
        /// <param name="menuItemId">MenuItem Unique Id.</param>
        /// <param name="name">Menu Item Name.</param>
        /// <param name="actionName">Action to where the Menu Item points.</param>
        /// <param name="controllerName">Controller to where the Menu Item points.</param>
        /// <param name="url">Url to the where the Menu Item points.</param>
        /// <param name="claimType">Menu Item Claim Type.</param>
        /// <param name="claimValue">Menu Item Claim Value.</param>
        /// <returns>See <see cref="MenuItem"/>.</returns>
        public static MenuItem FactorySeed(string menuItemId, string name, string actionName, string controllerName,
            string url, string claimType, string claimValue)
        {
            return new MenuItem
            {
                MenuItemId = Guid.Parse(menuItemId),
                Name = name,
                ActionName = actionName,
                ControllerName = controllerName,
                Url = url,
                ClaimType = claimType,
                ClaimValue = claimValue
            };
        }

        /// <summary>
        /// Factory used for MenuItem's Unit Tests.
        /// </summary>
        /// <param name="name">Menu Item Name.</param>
        /// <param name="actionName">Action to where the Menu Item points.</param>
        /// <param name="controllerName">Controller to where the Menu Item points.</param>
        /// <param name="url">Url to the where the Menu Item points.</param>
        /// <param name="claimType">Menu Item Claim Type.</param>
        /// <param name="claimValue">Menu Item Claim Value.</param>
        /// <returns>See <see cref="MenuItem"/>.</returns>
        public static MenuItem FactoryTest(string name, string actionName, string controllerName, string url,
            string claimType, string claimValue)
        {
            return new MenuItem
            {
                Name = name,
                ActionName = actionName,
                ControllerName = controllerName,
                Url = url,
                ClaimType = claimType,
                ClaimValue = claimValue
            };
        }
    }
}
