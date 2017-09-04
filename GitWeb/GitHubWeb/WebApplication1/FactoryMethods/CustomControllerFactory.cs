using DAL.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace WebApplication1.FactoryMethods
{
    public class CustomControllerFactory: IControllerFactory
    {
        private readonly string _controllerNamespace;
        public CustomControllerFactory(string controllerNamespace)
        {
            _controllerNamespace = controllerNamespace;
        }
        public IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            Type controllerType = Type.GetType(string.Concat(_controllerNamespace, ".", controllerName, "Controller"));
            dynamic service = new CustomerProductDAL();
            switch (controllerName)
            {
                case "CustomerProducts":
                    {
                        service = new CustomerProductDAL();
                        break;
                    }
                case "Orders":
                    {
                        service = new OrderDAL();
                        break;
                    }
                case "Home":
                    {
                        service = new DashBoardDAL();
                        break;
                    }
                case "Categories":
                    {
                        service = new CategoryDAL();
                        break;
                    }
                case "Products":
                    {
                        service = new ProductDAL();
                        break;
                    }
            }
            IController controller = Activator.CreateInstance(controllerType, new[] { service }) as Controller;
            return controller;



        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }
        public void ReleaseController(IController controller)
        {
            IDisposable disposable = controller as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }

    }
}