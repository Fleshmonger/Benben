using UnityEngine;

/// <summary> GameObject representation of a user-marked location for tile placement. </summary>
[RequireComponent(typeof(MeshRenderer))]
public class Mark : MonoBehaviour
{
    #region Fields

    public Material ValidMarkMaterial;
    public Material InvalidMarkMaterial;
    public MeshRenderer MeshRenderer;

    private bool _isValid;

    #endregion

    #region Properties

    /// <summary> Gets or sets the validity of the tile placement. </summary>
    public bool IsValid
    {
        get { return _isValid; }
        set
        {
            _isValid = value;
            var material = _isValid ? ValidMarkMaterial : InvalidMarkMaterial;
            MeshRenderer.material = material;
        }
    }

    #endregion
}