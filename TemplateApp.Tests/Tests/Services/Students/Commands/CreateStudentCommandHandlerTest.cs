using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TemplateApp.Application.Services.Students.Commands.CreateStudent;
using TemplateApp.Persistance;
using TemplateApp.Tests.Infrastructure;

namespace TemplateApp.Tests.Tests.Services.Students.Commands
{
    [TestClass]
    public class CreateStudentCommandHandlerTest : TestBase
    {
        [TestMethod]
        public void AddingRegularValues_ShouldCreateStudent()
        {
            var commandRequest = new CreateStudentCommand();
            commandRequest.CountryId = 1;
            commandRequest.Name = "Petar";
            commandRequest.Surname = "Petrovic";

            var handler = new CreateStudentCommandHandler(_context);
            var id = handler.Handle(commandRequest, new System.Threading.CancellationToken());

            Assert.IsNotNull(id);
            Assert.AreNotEqual(0, id);
        }
    }
}
