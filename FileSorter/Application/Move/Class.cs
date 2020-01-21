using MediatR;
using System;

namespace FileSorter.Application.Move
{
    public class MoveRequest : IRequest
    {
        public MoveRequest(string destination, params string[] paths)
        {
            Paths = paths ?? throw new ArgumentNullException(nameof(paths));
            Destination = destination;
        }

        public string[] Paths { get; }
        public string Destination { get; }
    }
}
