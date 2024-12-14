using UnityEngine;

public class MinimapIcon : MonoBehaviour {
    [SerializeField]
    private MeshRenderer _meshRenderer;

    [SerializeField]
    private Material _red, _blue;

    public void SetTeam(Team team) {
        _meshRenderer.material = team == Team.Blue ? _blue : _red;
    }
}