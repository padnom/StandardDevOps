﻿using System.Threading.Tasks;
using StandardDevOpsApi.Models.LibraryAccounts;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace StandardDevOpsApi.Tests.Unit.Services.Foundations.LibraryAccounts
{
    public partial class LibraryAccountServiceTests
    {
        [Fact]
        public async Task ShouldAddLibraryAccountAsync()
        {
            // given
            LibraryAccount randomLibraryAccount =
                CreateRandomLibraryAcocunt();

            LibraryAccount inputLibraryAccount =
                randomLibraryAccount;

            LibraryAccount insertedLibraryAccount =
                inputLibraryAccount;

            LibraryAccount expectedLibraryAccount =
                insertedLibraryAccount.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertLibraryAccountAsync(inputLibraryAccount))
                    .ReturnsAsync(insertedLibraryAccount);

            // when
            LibraryAccount actualLibraryAccount =
                await this.libraryAccountService.AddLibraryAccountAsync(
                    inputLibraryAccount);

            // then
            actualLibraryAccount.Should().BeEquivalentTo(expectedLibraryAccount);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertLibraryAccountAsync(inputLibraryAccount),
                    Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
