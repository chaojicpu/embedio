﻿using System;
using System.Threading;
using EmbedIO.Utilities;

namespace EmbedIO.WebApi
{
    public class WebApiModule : WebApiModuleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebApiModule" /> class.
        /// </summary>
        /// <param name="baseUrlPath">The base URL path served by this module.</param>
        /// <seealso cref="IWebModule.BaseUrlPath" />
        /// <seealso cref="Validate.UrlPath" />
        public WebApiModule(string baseUrlPath)
            : base(baseUrlPath)
        {
        }

        /// <summary>
        /// <para>Registers a controller type using a constructor.</para>
        /// <para>See <see cref="WebApiModuleBase.RegisterControllerType{TController}()"/>
        /// for further information.</para>
        /// </summary>
        /// <typeparam name="TController">The type of the controller.</typeparam>
        /// <seealso cref="RegisterController{TController}(Func{IHttpContext,CancellationToken,TController})"/>
        /// <seealso cref="RegisterController(Type)"/>
        /// <seealso cref="WebApiModuleBase.RegisterControllerType{TController}()"/>
        public void RegisterController<TController>()
            where TController : WebApiController
            => RegisterControllerType(typeof(TController));

        /// <summary>
        /// <para>Registers a controller type using a factory method.</para>
        /// <para>See <see cref="WebApiModuleBase.RegisterControllerType{TController}(Func{IHttpContext,CancellationToken,TController})"/>
        /// for further information.</para>
        /// </summary>
        /// <typeparam name="TController">The type of the controller.</typeparam>
        /// <param name="factory">The factory method used to construct instances of <typeparamref name="TController"/>.</param>
        /// <seealso cref="RegisterController{TController}()"/>
        /// <seealso cref="RegisterController(Type,Func{IHttpContext,CancellationToken,WebApiController})"/>
        /// <seealso cref="WebApiModuleBase.RegisterControllerType{TController}(Func{IHttpContext,CancellationToken,TController})"/>
        public void RegisterController<TController>(Func<IHttpContext, CancellationToken, TController> factory)
            where TController : WebApiController
            => RegisterControllerType(typeof(TController), factory);

        /// <summary>
        /// <para>Registers a controller type using a constructor.</para>
        /// <para>See <see cref="WebApiModuleBase.RegisterControllerType(Type)"/>
        /// for further information.</para>
        /// </summary>
        /// <param name="controllerType">The type of the controller.</param>
        /// <seealso cref="RegisterController(Type,Func{IHttpContext,CancellationToken,WebApiController})"/>
        /// <seealso cref="RegisterController{TController}()"/>
        /// <seealso cref="WebApiModuleBase.RegisterControllerType(Type)"/>
        public void RegisterController(Type controllerType)
            => RegisterControllerType(controllerType);

        /// <summary>
        /// <para>Registers a controller type using a factory method.</para>
        /// <para>See <see cref="WebApiModuleBase.RegisterControllerType(Type,Func{IHttpContext,CancellationToken,WebApiController})"/>
        /// for further information.</para>
        /// </summary>
        /// <param name="controllerType">The type of the controller.</param>
        /// <param name="factory">The factory method used to construct instances of <paramref name="controllerType"/>.</param>
        /// <seealso cref="RegisterController(Type)"/>
        /// <seealso cref="RegisterController{TController}(Func{IHttpContext,CancellationToken,TController})"/>
        /// <seealso cref="WebApiModuleBase.RegisterControllerType(Type,Func{IHttpContext,CancellationToken,WebApiController})"/>
        public void RegisterController(Type controllerType, Func<IHttpContext, CancellationToken, WebApiController> factory)
            => RegisterControllerType(controllerType, factory);
    }
}