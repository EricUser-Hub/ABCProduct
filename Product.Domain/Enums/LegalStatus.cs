using System.ComponentModel;

namespace Product.Domain.Enums
{
    public enum LegalStatus
    {
        [Description("none")]
        None = 0,
        
        [Description("1")]
        ControlledDrugPart2 = 1,  //  controllé

        [Description("2")]
        ControlledDrugPart1 = 2, //  controllé

        [Description("3")]
        ControlledDrugPart1Reportable = 3, //  controllé

        [Description("4")]
        NarcoticDrug = 4, //  narcotic

        [Description("5")]
        NarcoticDrugReportable = 5, //  narcotic

        [Description("6")]
        NarcoticPreparation = 6, // narcotic

        [Description("7")]
        NarcoticPreparationOtc = 7, //  narcotic

        [Description("9")]
        ControlledDrugPart3 = 8, //  controllé

        [Description("A")]
        ProfessionalAct = 9, 

        [Description("B")]
        TargetedSubstance = 10, //  ciblé

        [Description("P")]
        PrescriptionScheduleF = 11,

        [Description("U")]
        PrescriptionSchedule1 = 12,

        [Description("H")]
        Homeopathy = 13,

        [Description("G")]
        OtcFreeSale = 14,

        [Description("D")]
        OtcSchedule2 = 15,

        [Description("T")]
        OtcSchedule3 = 16
    }
}

