using MediatR;
using Microsoft.EntityFrameworkCore;
using notes.application.Common.Exceptions;
using notes.application.Interfaces;
using notes.domain;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace notes.application.Notes.Commands.DeleteNote
{
    public class DeleteNoteCommandHandler
        : IRequestHandler<DeleteNoteCommand>
    {
        private readonly INotesDbContext _dbContext;

        public DeleteNoteCommandHandler(INotesDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteNoteCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Notes
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            _dbContext.Notes.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
