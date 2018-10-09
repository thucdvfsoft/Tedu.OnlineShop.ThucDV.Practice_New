using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CKSource.CKFinder.Connector.Config;
using CKSource.CKFinder.Connector.Core.Acl;
using CKSource.CKFinder.Connector.Core.Builders;
using CKSource.CKFinder.Connector.Host.Owin;
using CKSource.FileSystem.Local;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.StaticFiles.ContentTypes;
using Owin;
using Tedu.OnlineShop;

[assembly: OwinStartupAttribute(typeof(MVCIntegrationExample.Startup))]
namespace MVCIntegrationExample
{
    public partial class Startup
    {
        //public void Configuration(IAppBuilder app)
        //{
        //    /*
        //     * If you installed CKSource.CKFinder.Connector.Logs.NLog you can start the logger:
        //     * LoggerManager.LoggerAdapterFactory = new NLogLoggerAdapterFactory();
        //     * Keep in mind that the logger should be initialized only once and before any other
        //     * CKFinder method is invoked.
        //     */

        //    /*
        //     * Register the "local" type backend file system.
        //     */
        //    //FileSystemFactory.RegisterFileSystem<LocalStorage>();

        //    /*
        //     * Map the CKFinder connector service under a given path. By default the CKFinder JavaScript
        //     * client expects the ASP.NET connector to be accessible under the "/ckfinder/connector" route.
        //     */
        //    app.Map("/ckfinder/connector", SetupConnector);

        //}

        /*
           * Simple configuration of the connector.
           */
        private class MyContentTypeProvider : FileExtensionContentTypeProvider
        {
            public MyContentTypeProvider()
            {
                Mappings.Add(".json", "application/json");
            }
        }
        public ConnectorBuilder ConfigureConnector()
        {
            var connectorBuilder = new ConnectorBuilder();
            var customAuthenticator = new MyAuthenticator();
            connectorBuilder
                .SetAuthenticator(customAuthenticator)
                .SetRequestConfiguration(
                    (request, config) =>
                    {
                         /* Add a local backend. */
                        config.AddProxyBackend("local", new LocalStorage(@"MyFiles"));

                         /* Add a resource type that uses the local backend. */
                        config.AddResourceType("Files", resourceBuilder => resourceBuilder.SetBackend("local", "files"));

                         /* Give full access to all resource types at any path for all users. */
                        config.AddAclRule(new AclRule(
                            new StringMatcher("*"), new StringMatcher("/"), new StringMatcher("*"),
                            new Dictionary<Permission, PermissionType>
                            {
                                 { Permission.FolderView, PermissionType.Allow },
                                 { Permission.FolderCreate, PermissionType.Allow },
                                 { Permission.FolderRename, PermissionType.Allow },
                                 { Permission.FolderDelete, PermissionType.Allow },

                                 { Permission.FileView, PermissionType.Allow },
                                 { Permission.FileCreate, PermissionType.Allow },
                                 { Permission.FileRename, PermissionType.Allow },
                                 { Permission.FileDelete, PermissionType.Allow },

                                 { Permission.ImageResize, PermissionType.Allow },
                                 { Permission.ImageResizeCustom, PermissionType.Allow }
                            }));
                    });

            /* Set the authenticator. */
            //connectorBuilder.SetAuthenticator(new MyAuthenticator());

            return connectorBuilder;
        }

        /*
         * Owin configuration.
         */
        public void Configuration(IAppBuilder appBuilder)
        {
            /* Configure the connector builder. */
            var connectorBuilder = ConfigureConnector();

            /* Build connector middleware. */
            var connector = connectorBuilder.Build(new OwinConnectorFactory());

            /* Map connector middleware to the /CKFinder/connector route. */
            appBuilder.Map("/CKFinder/connector", builder => builder.UseConnector(connector));

            /* Configure access to local files for JavaScript files. */
            var options = new FileServerOptions();
            options.StaticFileOptions.ContentTypeProvider = new MyContentTypeProvider();
            options.FileSystem = new PhysicalFileSystem("./CKFinderScripts");

            /* Map local files at the root path. */
            appBuilder.UseFileServer(options);
        }
    }
}