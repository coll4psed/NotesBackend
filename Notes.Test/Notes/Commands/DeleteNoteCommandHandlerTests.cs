using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Notes.Commands.DeleteNote;
using Notes.Test.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Test.Notes.Commands
{
    public class DeleteNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteNoteCommandHandler_Success()
        {
            // Assert
            var handler = new DeleteNoteCommandHandler(Context);

            // Act
            await handler.Handle(new DeleteNoteCommand
            {
                Id = NotesContextFactory.NoteIdForDelete,
                UserId = NotesContextFactory.UserAId
            }, CancellationToken.None);

            // Assert
            Assert.Null(Context.Notes.SingleOrDefaultAsync(note =>
            note.Id == NotesContextFactory.NoteIdForDelete));
        }

        [Fact]
        public async Task DeleteNoteCommandHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new DeleteNoteCommandHandler(Context);

            //Act


            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(
                new DeleteNoteCommand
                {
                    Id = Guid.NewGuid(),
                    UserId = NotesContextFactory.UserAId
                }, CancellationToken.None));
        }

        [Fact]
        public async Task DeleteNoteCommandHandler_FailOnWrongUserId()
        {

        }
    }
}
