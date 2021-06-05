using Microsoft.VisualStudio.TestTools.UnitTesting;
using SymphTags.Tests.Infrastructure;
using SymphTagsApp.Application.Exceptions;
using SymphTagsApp.Application.Services.Links.Commands.CreateLink;

namespace SymphTags.Tests.Tests.Services.Links.Commands
{
    [TestClass]
    public class CreateLinkCommandHandlerTest : TestBase
    {
        [TestMethod]
        public void AddingRegularValues_ShouldCreateLink()
        {
            var commandRequest = new CreateLinkCommand();
            commandRequest.Url = "www.google.com";
            commandRequest.Tags = new System.Collections.Generic.List<string> { "search", "engine" } ;

            var handler = new CreateLinkCommandHandler(_context, _currentUser);
            var id = handler.Handle(commandRequest, new System.Threading.CancellationToken());

            Assert.IsNotNull(id);
            Assert.AreNotEqual(0, id);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task AddingUrlThatAlreadyExistsForUser_ShouldNotCreateLinkAsync()
        {
            var myContext = _context;

            myContext.Link.Add(new Domain.Entities.Link
            {
                Url = "www.google.com",
                UserIdCreated = _currentUser.Id
            });

            myContext.SaveChanges();

            var commandRequest = new CreateLinkCommand();
            commandRequest.Url = "www.google.com";
            commandRequest.Tags = new System.Collections.Generic.List<string> { "search", "engine" };

            var handler = new CreateLinkCommandHandler(myContext, _currentUser);

            var exception = await Assert.ThrowsExceptionAsync<DomainErrorException>( async () =>
            {
                await handler.Handle(commandRequest, new System.Threading.CancellationToken());
            });

            Assert.AreEqual("This url already exists for this user.", exception.Message);
        }

    }
}
