using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using TestProject.DataProcess.Repository.Base;
using TestProject.DataProcess.Repository.Interfaces;


namespace TestProject.App_Start
{
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion
        
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IAdminRepository,AdminRepository>();
        }
    }
}
