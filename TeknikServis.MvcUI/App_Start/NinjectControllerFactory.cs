using Ninject;
using TeknikServis.Bll;
using TeknikServis.Dal.Concrete.EntityFramework.Repository;
using TeknikServis.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TeknikServis.MvcUI.App_Start
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel kernel;
        public NinjectControllerFactory()
        {
            kernel = new StandardKernel();

            AddBllBinds();
        }

        private void AddBllBinds()
        {
            this.kernel.Bind<IKullaniciService>().To<KullaniciManager>().WithConstructorArgument("kullaniciRepository", new EfKullaniciRepository());
            this.kernel.Bind<IFirmaService>().To<FirmaManager>().WithConstructorArgument("firmaRepository", new EfFirmaRepository());
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)kernel.Get(controllerType);
        }
    }
}