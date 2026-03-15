using Einstein.model;
using Einstein.model.json;
using phi.graphics.renderables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Einstein.model.json.JsonNeuron;
using static Einstein.ui.editarea.NeuronValueCalculator;

namespace Einstein.config.bibiteVersions.vanilla
{
    public class BibiteVersion1_3a4 : BibiteVanillaVersion
    {
        internal static readonly BibiteVersion1_3a4 INSTANCE = new BibiteVersion1_3a4();

        private BibiteVersion1_3a4(): base(100)
        {
            VERSION_NAME = "1_3a4";

            INPUT_NODES_INDEX_MIN = 0;
            INPUT_NODES_INDEX_MAX = 27;
            OUTPUT_NODES_INDEX_MIN = 28;
            OUTPUT_NODES_INDEX_MAX = 43;
            HIDDEN_NODES_INDEX_MIN = 44;
            HIDDEN_NODES_INDEX_MAX = int.MaxValue;

            DESCRIPTIONS = new string[] {
                // ----- Inputs -----
                "Constant (Input)",
                "EnergyRatio (Input)",
                "Maturity (Input)",
                "LifeRatio (Input)",
                "Speed (Input)",
                "AttackedDamage (Input)",
                "BibiteConcentrationWeight (Input)",
                "BibiteConcentrationAngle (Input)",
                "NVisibleBibites (Input)",
                "PelletConcentrationWeight (Input)",
                "PelletConcentrationAngle (Input)",
                "NVisiblePellets (Input)",
                "MeatConcentrationWeight (Input)",
                "MeatConcentrationAngle (Input)",
                "NVisibleMeat (Input)",
                "ClosestBibiteR (Input)",
                "ClosestBibiteG (Input)",
                "ClosestBibiteB (Input)",
                "Tic (Input)",
                "Minute (Input)",
                "TimeAlive (Input)",
                "PheroSense1 (Input)",
                "PheroSense2 (Input)",
                "PheroSense3 (Input)",
                "Phero1Angle (Input)",
                "Phero2Angle (Input)",
                "Phero3Angle (Input)",
                "InfectionRate (Input)",
                // ----- Outputs -----
                "Accelerate (TanH)",
                "Rotate (TanH)",
                "Herding (TanH)",
                "Want2Lay (Sigmoid)",
                "Want2Eat (Sigmoid)",
                "Want2Sex (Sigmoid)",
                "Grab (Sigmoid)",
                "ClkReset (Sigmoid)",
                "PhereOut1 (ReLU)",
                "PhereOut2 (ReLU)",
                "PhereOut3 (ReLU)",
                "Want2Grow (Sigmoid)",
                "Want2Heal (Sigmoid)",
                "Want2Attack (Sigmoid)",
                "ImmuneSystem (TanH)",
            };

            outputTypes = new NeuronType[]
            {
                NeuronType.TanH,
                NeuronType.TanH,
                NeuronType.TanH,
                NeuronType.Sigmoid,
                NeuronType.Sigmoid,
                NeuronType.Sigmoid,
                NeuronType.Sigmoid,
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
                NeuronType.Sigmoid,
                NeuronType.Linear,
                NeuronType.TanH,
                NeuronType.Sine,
                NeuronType.ReLu,
                NeuronType.Gaussian,
                NeuronType.Latch,
                NeuronType.Differential,
            };
        }
        #region Version Name Matching

        protected override bool IsMatchForVersionName(string bibitesVersionName)
        {
            if (bibitesVersionName.Equals("1.3a4"))
            {
                return true;
            }
            return false;
        }

        #endregion Version Name Matching

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
