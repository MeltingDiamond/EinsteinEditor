using Einstein.model;
using Einstein.model.json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Einstein.model.json.JsonNeuron;
using static Einstein.ui.editarea.NeuronValueCalculator;

namespace Einstein.config.bibiteVersions.vanilla
{
    public class BibiteVersion0_3 : BibiteVanillaVersion
    {
        internal static readonly BibiteVersion0_3 INSTANCE = new BibiteVersion0_3();

        private BibiteVersion0_3(): base(300)
        {
            VERSION_NAME = "0.3";

            INPUT_NODES_INDEX_MIN = 0;
            INPUT_NODES_INDEX_MAX = 28;
            OUTPUT_NODES_INDEX_MIN = 29;
            OUTPUT_NODES_INDEX_MAX = 43;
            HIDDEN_NODES_INDEX_MIN = 44;
            HIDDEN_NODES_INDEX_MAX = int.MaxValue;

            DESCRIPTIONS = new string[] {
                // ----- Inputs -----
                "Constant",
                "EnergyRatio",
                "Maturity",
                "LifeRatio",
                "Speed",
                "IsGrabbingObjects",
                "AttackedDamage",
                "BibiteConcentrationWeight",
                "BibiteConcentrationAngle",
                "NVisibleBibites",
                "PelletConcentrationWeight",
                "PelletConcentrationAngle",
                "NVisiblePellets",
                "MeatConcentrationWeight",
                "MeatConcentrationAngle",
                "NVisibleMeat",
                "ClosestBibiteR",
                "ClosestBibiteG",
                "ClosestBibiteB",
                "Tic",
                "Minute",
                "TimeAlive",
                "PheroSense1",
                "PheroSense2",
                "PheroSense3",
                "Phero1Angle",
                "Phero2Angle",
                "Phero3Angle",
                "InfectionRate",
                // ----- Outputs -----
                "Accelerate",
                "Rotate",
                "Herding",
                "Want2Lay",
                "Want2Eat",
                "Want2Sex",
                "Grab",
                "ClkReset",
                "PhereOut1",
                "PhereOut2",
                "PhereOut3",
                "Want2Grow",
                "Want2Heal",
                "Want2Attack",
                "ImmuneSystem",
            };

            outputTypes = new NeuronType[]
            {
                NeuronType.TanH,
                NeuronType.TanH,
                NeuronType.TanH,
                NeuronType.Sigmoid,
                NeuronType.Sigmoid,
                NeuronType.Sigmoid,
                NeuronType.TanH,
                NeuronType.Sigmoid,
                NeuronType.ReLu,
                NeuronType.ReLu,
                NeuronType.ReLu,
                NeuronType.Sigmoid,
                NeuronType.Sigmoid,
                NeuronType.Sigmoid,
                NeuronType.TanH,
            };

            neuronTypes = new NeuronType[]
            {
                NeuronType.Input,
                NeuronType.Sigmoid,//
                NeuronType.Linear,//
                NeuronType.TanH,//
                NeuronType.Sine,
                NeuronType.ReLu, //
                NeuronType.Gaussian,//
                NeuronType.Latch,//
                NeuronType.Differential,//
                NeuronType.Abs,
                NeuronType.Mult,
            };
        }

        #region Brain Calculations

        public override bool HasBiases()
        {
            return false;
        }

        public override DeltaTimeCalcMethod GetDeltaTimeCalcMethod()
        {
            return DeltaTimeCalcMethod.SimSpeed;
        }

        public override SynapseFiringCalcMethod GetSynapseOrderCalcMethod()
        {
            return SynapseFiringCalcMethod.InOrder;
        }

        #endregion Brain Calculations

        #region Neuron diagram positions

        public override bool GetNeuronDiagramPositionFromRawJsonFields(RawJsonFields fields, ref int x, ref int y)
        {
            // fall back to inov if it's not in the description
            return GetNeuronDiagramPositionFromDescription(fields, ref x, ref y)
                || GetNeuronDiagramPositionFromInov(fields, ref x, ref y);
        }
        public override void SetNeuronDiagramPositionInRawJsonFields(RawJsonFields fields, int x, int y)
        {
            SetNeuronDiagramPositionInDesc(fields, x, y);
            // always set inov
            SetNeuronDiagramPositionInInov(fields, x, y);
        }

        #endregion Neuron diagram positions

        #region Converting Between Versions

        protected override BaseBrain CreateVersionDownCopyOf(BaseBrain brain)
        {
            if (brain.BibiteVersion != this)
            {
                throw new ArgumentException($"source brain version ({brain.BibiteVersion.VERSION_NAME}) does not match the converting version ({VERSION_NAME})");
            }
            throw new NoSuchVersionException("There is no supported version lower than " + VERSION_NAME);
        }

        protected override BaseBrain CreateVersionUpCopyOf(BaseBrain brain)
        {
            if (brain.BibiteVersion != this)
            {
                throw new ArgumentException($"source brain version ({brain.BibiteVersion.VERSION_NAME}) does not match the converting version ({VERSION_NAME})");
            }
            // To 0.5
            // deep copy with no changes
            return new JsonBrain(brain, V0_5);
        }

        #endregion Converting Between Versions
    }
}
