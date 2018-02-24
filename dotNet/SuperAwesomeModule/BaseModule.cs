using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SuperAwesomeModule
{
    public abstract class BaseHttpModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {

            context.PreRequestHandlerExecute += (sender, e) => PreRequestHandlerExecute(new HttpContextWrapper(((HttpApplication) sender).Context));
            context.BeginRequest += (sender, e) => ApplicationBeginRequest(new HttpContextWrapper(((HttpApplication)sender).Context));
            context.Error += (sender, e) => OnError(new HttpContextWrapper(((HttpApplication)sender).Context));
            context.EndRequest += (sender, e) => ApplicationEndRequest(new HttpContextWrapper(((HttpApplication)sender).Context));
        }

        public void Dispose()
        {
        }

        public virtual void PreRequestHandlerExecute(HttpContextBase context) { }

        public virtual void ApplicationBeginRequest(HttpContextBase context) { }

        public virtual void OnError(HttpContextBase context) { }

        public virtual void ApplicationEndRequest(HttpContextBase context) {}
    }
}
