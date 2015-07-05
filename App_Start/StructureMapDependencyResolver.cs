using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using StructureMap;

namespace BootstrapVillas.App_Start
{
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        private StructureMap.IContainer _container;

        public StructureMapDependencyResolver(StructureMap.IContainer container)
        {
            _container = container;
        }


        public object GetService(Type serviceType)
        {
            object instance = _container.TryGetInstance(serviceType);
            return instance;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }
    }
}
