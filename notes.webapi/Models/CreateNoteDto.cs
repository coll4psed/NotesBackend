using AutoMapper;
using notes.application.Common.Mappings;
using notes.application.Notes.Commands.CreateNote;

namespace notes.webapi.Models
{
    public class CreateNoteDto : IMapWith<CreateNoteDto>
    {
        public string Title { get; set; }
        public string Details { get; set; }

        public void Mapping (Profile profile)
        {
            profile.CreateMap<CreateNoteDto, CreateNoteCommand>()
                .ForMember(noteCommand => noteCommand.Title,
                opt => opt.MapFrom(noteDto => noteDto.Title))
                .ForMember(noteCommand => noteCommand.Details,
                opt => opt.MapFrom(noteDto => noteDto.Details));
        }
    }
}
