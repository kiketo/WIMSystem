﻿using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.Utils;
using WIMSystem.Core.Factories.Contracts;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Commands.CreateCommands
{
    public class CreateFeedbackCommand : IEngineCommand
    {

        private readonly IHistoryEventWriter historyEventWriter;
        private readonly IComponentsFactory componentsFactory;
        private readonly IGetters getter;

        public CreateFeedbackCommand(IHistoryEventWriter historyEventWriter, IPersonsCollection personList, IComponentsFactory componentsFactory, IGetters getter)
        {
            this.historyEventWriter = historyEventWriter ?? throw new ArgumentNullException(nameof(historyEventWriter));
            this.componentsFactory = componentsFactory ?? throw new ArgumentNullException(nameof(componentsFactory));
            this.getter = getter ?? throw new ArgumentNullException(nameof(getter));
        }

        public string ReadSingleCommand(IList<string> parameters)
        {
            var feedbackTitle = parameters[0];
            var feedbackDescription = parameters[1];
            var feedbackRating = int.Parse(parameters[2]);
            var teamName = parameters[3];
            var board = this.getter.GetBoard(teamName, parameters[4]);

            return this.CreateFeedback(feedbackTitle, feedbackDescription, feedbackRating, board);
        }

        private string CreateFeedback(string feedbackTitle, string feedbackDescription, int raiting, IBoard board)
        {
            var feedback = this.componentsFactory.CreateFeedback(feedbackTitle, feedbackDescription, raiting, board);

            if (feedback == null)
            {
                throw new ArgumentException(string.Format(ObjectConsts.ObjectExists, nameof(Feedback), feedback.Title));
            }

            board.AddWorkItemToBoard(feedback);

            string returnMessage = string.Format(ObjectConsts.ObjectCreated, nameof(Feedback), feedback.Title);

            this.historyEventWriter.AddHistoryEvent(returnMessage, board: board, team: board.Team);

            return returnMessage;
        }

    }
}
