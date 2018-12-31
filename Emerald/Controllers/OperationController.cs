using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Emerald.Controllers
{
    public class OperationController : Controller
    {
        private readonly OperationService _operationService;
        private readonly IOperationTransient _transientOperation;
        private readonly IOperationScoped _scopedOperation;
        private readonly IOperationSingleton _singletonOperation;
        private readonly IOperationSingletonInstance _singletonInstanceOperation;

        public OperationController(
            OperationService operationService,
            IOperationTransient transientOperation,
            IOperationScoped scopedOperation,
            IOperationSingleton singletonOperation,
            IOperationSingletonInstance singletonInstanceOperation)
        {
            _operationService = operationService;
            _transientOperation = transientOperation;
            //Scoped objects are the same within a request but different across requests.
            _scopedOperation = scopedOperation;
            //Singleton objects are the same for every object and every request regardless of whether an Operation instance is provided in ConfigureServices.
            _singletonOperation = singletonOperation;
            _singletonInstanceOperation = singletonInstanceOperation;
        }
        public IActionResult Index()
        {
            //HttpContext.(This yields classes that are easier to test.)
            //Generally, the app shouldn't use these properties directly. Instead, request the types that classes require via class constructors and allow the framework inject the dependencies.
            //HttpContext.RequestServices

            ViewBag.Transient = _transientOperation;
            ViewBag.Scoped = _scopedOperation;
            //OperationId在重启后还是相同，说明应用程序实际还是运行着（寄宿在IIS Express）。
            //重启IIS后，变化。
            ViewBag.Singleton = _singletonOperation;
            ViewBag.SingletonInstance = _singletonInstanceOperation;
            ViewBag.Service = _operationService;
            return View();
        }
    }
}