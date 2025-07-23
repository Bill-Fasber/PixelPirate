using PixelPirateCodes.Model.Data;
using UnityEngine;

namespace PixelPirateCodes.Model.Definitions.Repositories.Dialogs
{
    [CreateAssetMenu(menuName = "Defs/Dialog", fileName = "Dialog")]
    public class DialogDef : ScriptableObject
    {
        [SerializeField] private DialogData _data;
        public DialogData Data => _data;
    }
}