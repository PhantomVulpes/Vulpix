﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vulpix.Domain.Commands;
using Vulpix.Test.Helpers;
using Vulpix.Test.TestDoubles;

namespace Vulpix.Test.Tests.Commands
{
    [TestClass]
    public class DeleteItemCommandTests
    {
        private readonly TestHobbyFile testHobbyFile;

        private readonly DeleteItemCommandHandler underTest;

        public DeleteItemCommandTests()
        {
            testHobbyFile = new(ItemHelper.GenerateItem());

            underTest = new();
        }

        [TestMethod]
        public async Task ItemGetsDeleted()
        {
            await underTest.ExecuteAsync(new(testHobbyFile, testHobbyFile.Items.First()));
            Assert.IsTrue(testHobbyFile.DeletedItems.Any());
        }
    }
}