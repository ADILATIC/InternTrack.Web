﻿// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using InternTrack.Portal.Web.Models.Interns;
using InternTrack.Portal.Web.Models.InternViews;
using InternTrack.Portal.Web.Models.InternViews.Exceptions;
using Moq;
using Xunit;

namespace InternTrack.Portal.Web.Tests.Unit.Services.Foundations.InternViews
{
    public partial class InternViewServiceTests
    {
        [Theory]
        [MemberData(nameof(InternServiceValidationExceptions))]
        private async Task
            ShouldThrowDependencyValidationExceptionOnAddIfInternValidationErrorOccurredAndLogItAsync(
                Exception internServiceValidationException)
        {
            // given
            InternView someInternView = CreateRandomInternView();

            var expectedDependencyValidationException =
                new InternViewDependencyValidationException(
                    message: "Intern View dependency validation error occurred, try again.",
                        innerException: internServiceValidationException);

            this.internServiceMock.Setup(service =>
                service.AddInternAsync(It.IsAny<Intern>()))
                    .ThrowsAsync(internServiceValidationException);

            // when
            ValueTask<InternView> addInternViewTask =
                this.internViewService.AddInternViewAsync(someInternView);

            InternViewDependencyValidationException actualDependencyValidationException =
                await Assert.ThrowsAsync<InternViewDependencyValidationException>(() =>
                    addInternViewTask.AsTask());

            // then
            actualDependencyValidationException.Should()
                .BeEquivalentTo(expectedDependencyValidationException);

            this.userServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInUser(),
                    Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.internServiceMock.Verify(service =>
                service.AddInternAsync(It.IsAny<Intern>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedDependencyValidationException))),
                        Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.internServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(InternServiceDependencyExceptions))]
        private async Task 
            ShouldThrowDependencyExceptionOnAddIfInternDependencyErrorOccurredAndLogItAsync(
            Exception internServiceDependencyException)
        {
            // given
            InternView someInternView = CreateRandomInternView();

            var expectedDependencyException =
                new InternViewDependencyException(
                    message: "Intern View dependency validation error occurred, try again.",
                        innerException: internServiceDependencyException);

            this.internServiceMock.Setup(service =>
                service.AddInternAsync(It.IsAny<Intern>()))
                    .ThrowsAsync(internServiceDependencyException);

            // when
            ValueTask<InternView> addInternViewTask =
                this.internViewService.AddInternViewAsync(someInternView);

            InternViewDependencyException actualDependencyValidationException =
                await Assert.ThrowsAsync<InternViewDependencyException>(() =>
                    addInternViewTask.AsTask());

            // then
            actualDependencyValidationException.Should()
                .BeEquivalentTo(expectedDependencyException);

            this.userServiceMock.Verify(service =>
                service.GetCurrentlyLoggedInUser(),
                    Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.internServiceMock.Verify(service =>
                service.AddInternAsync(It.IsAny<Intern>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedDependencyException))),
                        Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.userServiceMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.internServiceMock.VerifyNoOtherCalls();
        }
    }
}
