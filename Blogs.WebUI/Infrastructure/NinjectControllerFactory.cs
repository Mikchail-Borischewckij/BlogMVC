using System;
using System.Web.Mvc;
using System.Web.Routing;
using Blogs.Domain.Abstract;
using Blogs.Domain.Concrete;
using Ninject;

namespace Blogs.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel _ninjectKernel;

        public NinjectControllerFactory()
        {
            _ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)_ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            _ninjectKernel.Bind<IUserRepository>().To<UsersRepository>();
            _ninjectKernel.Bind<IPostRepository>().To<PostRepository>();
            _ninjectKernel.Bind<ICommentsRepository>().To<CommentsRepository>();
            _ninjectKernel.Bind<ILikeRepository>().To<LikeRepository>();
        }
     
       

    }
}