using Flashcards.Application.Interfaces;
using Flashcards.Domain.Commands;
using Flashcards.Domain.Interfaces.Repositories;
using Flashcards.Domain.Notifications;
using Flashcards.Domain.Responses;
using MediatR;

namespace Flashcards.Application.Handlers
{
    public class ClassLessonListCommandHandler : IRequestHandler<ClassLessonListCommand, IEnumerable<ClassLessonListResponse>>
    {
        private readonly IClassRepository _classRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IMediator _mediator;
        private readonly IUserContextService _userContextService;

        public ClassLessonListCommandHandler(
            IClassRepository classRepository,
            ILessonRepository lessonRepository,
            IMediator mediator,
            IUserContextService userContextService)
        {
            _classRepository = classRepository;
            _lessonRepository = lessonRepository;
            _mediator = mediator;
            _userContextService = userContextService;
        }

        public async Task<IEnumerable<ClassLessonListResponse>> Handle(ClassLessonListCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await _mediator.Publish(new ValidationErrorNotification(request.ValidationResult));
            }
            var user = _userContextService.GetUserContext();

            if (user.Id != request.IdUser)
            {
                await _mediator.Publish(new ValidationErrorNotification("Usuário inválido para vizualizar as aulas"));
            }

            var response = new List<ClassLessonListResponse>();
            var classes = await _classRepository.ListWithQtdLessonsAsync(request.IdUser);

            if (classes != null)
            {

                foreach (var cl in classes.ToList())
                {
                    var classLesson = new ClassLessonListResponse();

                    classLesson.IdClass = cl.Id;
                    classLesson.Name = cl.Name;

                    var lesson = await _lessonRepository.ListWithQtdFlashcardAsync(cl.Id);

                    classLesson.Lessons = new List<LessonResponse>();

                    if (lesson != null && lesson.Count() > 0)
                    {

                        foreach (var les in lesson.ToList())
                        {
                            var lessonResponse = new LessonResponse();

                            lessonResponse.IdLesson = les.IdLesson;
                            lessonResponse.Name = les.Name;
                            lessonResponse.Quantity = les.Quantity;

                            classLesson.Lessons.Add(lessonResponse);
                        }

                        response.Add(classLesson);
                    }


                }
            }

            return response;

        }
    }
}
