using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


// Para usar, colocar o pai com máscara normalmente (ele será o Mask).
// Como filho, colocar o Masked, com esse componente ao invés do image;
 
public class InvertedMaskImage : Image
{
    public override Material materialForRendering
    {
        get
        {
            Material result = Instantiate(base.materialForRendering);
            result.SetInt("_StencilComp", (int)CompareFunction.NotEqual);
            return result;
        }
    }
}
