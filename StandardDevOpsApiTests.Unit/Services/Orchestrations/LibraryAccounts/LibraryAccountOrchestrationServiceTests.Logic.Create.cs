﻿using FluentAssertions;

using Force.DeepCloner;

using Moq;

using StandardDevOpsApi.Models.LibraryAccounts;
using StandardDevOpsApi.Models.LibraryCards;

using Xunit;

namespace StandardDevOpsApi.Tests.Unit.Services.Orchestrations.LibraryAccounts
{
    public partial class LibraryAccountOrchestrationServiceTests
    {
        [Fact]
        public async Task ShouldCreateLibraryAccountAsync()
        {
            // given
            LibraryAccount randomLibraryAccount =
                CreateRandomLibraryAccount();

            LibraryAccount inputLibraryAccount =
                randomLibraryAccount;

            LibraryAccount addedLibraryAccount =
                inputLibraryAccount;

            LibraryAccount expectedLibraryAccount =
                addedLibraryAccount.DeepClone();

            var mockSequence = new MockSequence();

            var expectedInputLibraryCard = new LibraryCard
            {
                Id = Guid.NewGuid(),
                LibraryAccountId = addedLibraryAccount.Id
            };

            this.libraryAccountServiceMock.InSequence(mockSequence).Setup(service =>
                service.AddLibraryAccountAsync(inputLibraryAccount))
                    .ReturnsAsync(addedLibraryAccount);

            this.libraryCardServiceMock.InSequence(mockSequence).Setup(service =>
                service.AddLibraryCardAsync(It.Is(
                    SameLibraryCardAs(expectedInputLibraryCard))))
                        .ReturnsAsync(expectedInputLibraryCard);

            // when
            LibraryAccount actualLibraryAccount =
                await this.libraryAccountOrchestrationService
                    .CreateLibraryAccountAsync(inputLibraryAccount);

            // then
            actualLibraryAccount.Should().BeEquivalentTo(expectedLibraryAccount);

            this.libraryAccountServiceMock.Verify(service =>
                service.AddLibraryAccountAsync(inputLibraryAccount),
                    Times.Once);

            this.libraryCardServiceMock.Verify(broker =>
                broker.AddLibraryCardAsync(It.Is(SameLibraryCardAs(
                    expectedInputLibraryCard))),
                        Times.Once);

            this.libraryAccountServiceMock.VerifyNoOtherCalls();
            this.libraryCardServiceMock.VerifyNoOtherCalls();
        }
    }
}