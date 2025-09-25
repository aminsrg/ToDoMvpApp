using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoMvpApp.Application.Commands.ToDo.Update;

namespace ToDoMvpApp.Application.Commands.ToDo.Delete;

public record DeleteToDoCommand(string Id) : IRequest;



