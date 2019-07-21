using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using School.Common.Command;
using School.Common.Event;
using School.Common.Handler.Interfaces;
using School.Services.Courses.Dtos.Author;
using School.Services.Courses.Services.Interfaces;

namespace School.Services.Courses.Handler
{
    public class CreateAuthorCommandHandler :ICommandHandler<CreateAuthorCommand>
    {
        private readonly IBusClient _busClient;
        private readonly IServiceScopeFactory _serviceScopeFactory;


        public CreateAuthorCommandHandler(IBusClient busClient, IServiceScopeFactory serviceScopeFactory)
        {
            _busClient = busClient;
            _serviceScopeFactory = serviceScopeFactory;
           
      
        }
        public async Task HandleAsync(CreateAuthorCommand command)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                Console.WriteLine(command.AuthorName);
         
                var _authorService = scope.ServiceProvider.GetService<IAuthorService>();
                Console.WriteLine("2");
                var _mapper = scope.ServiceProvider.GetService<IMapper>();
                Console.WriteLine("3");
                try
                {
                    var dto = _authorService.CreateAuthor(_mapper.Map<CreateAuthorCommand, CreateAuthorDto>(command),
                        command.UserId);
                    await _busClient.PublishAsync(_mapper.Map<AuthorDto, AuthorCreatedEvent>(dto));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
             
               

            }
            

        }
    }
}   