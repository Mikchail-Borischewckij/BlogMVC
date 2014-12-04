using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Blogs.Domain.Entities;
using Blogs.Domain.Abstract;
using Blogs.Domain.Concrete;

namespace Blogs.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IUserRepository>().To<UsersRepository>();
            ninjectKernel.Bind<IPostRepository>().To<PostRepository>();
            ninjectKernel.Bind<ICommentsRepository>().To<CommentsRepository>();

        }
     
       

    }
}