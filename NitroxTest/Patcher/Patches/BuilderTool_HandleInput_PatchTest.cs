﻿using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NitroxPatcher.Patches;
using Harmony;
using NitroxTest.Patcher.Test;
using System.Linq;

namespace NitroxTest.Patcher.Patches
{
    class BuilderTool_HandleInput_PatchTest
    {
        [TestMethod]
        public void Sanity()
        {
            List<CodeInstruction> instructions = PatchTestHelper.GenerateDummyInstructions(100);
            instructions.Add(new CodeInstruction(BuilderTool_HandleInput_Patch.INJECTION_OPCODE, BuilderTool_HandleInput_Patch.INJECTION_OPERAND));

            IEnumerable<CodeInstruction> result = BuilderTool_HandleInput_Patch.Transpiler(null, instructions);

            Assert.AreEqual(instructions.Count + 3, result.Count());
        }

        [TestMethod]
        public void InjectionSanity()
        {
            List<CodeInstruction> beforeInstructions = PatchTestHelper.GetInstructionsFromMethod(BuilderTool_HandleInput_Patch.TARGET_METHOD);
            IEnumerable<CodeInstruction> result = BuilderTool_HandleInput_Patch.Transpiler(BuilderTool_HandleInput_Patch.TARGET_METHOD, beforeInstructions);

            Assert.IsTrue(beforeInstructions.Count < result.Count());
        }
    }
}
