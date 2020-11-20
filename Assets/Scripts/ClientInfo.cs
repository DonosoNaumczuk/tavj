using System.Net;
using UnityEngine;

public class ClientInfo
{
    private readonly int _id;
    private readonly IPEndPoint _endPoint;
    private readonly GameObject _entity;
    
    private PlayerInput _playerInput;

    public ClientInfo(int id, IPEndPoint endPoint, GameObject entity)
    {
        _id = id;
        _endPoint = endPoint;
        _entity = entity;
        _playerInput = new PlayerInput();
    }

    public int Id => _id;

    public IPEndPoint EndPoint => _endPoint;

    public GameObject Entity => _entity;
    
    public PlayerInput PlayerInput => _playerInput;

    public void SerializeIntoBuffer(BitBuffer buffer)
    {
        buffer.PutInt(_id);
        var transform = _entity.transform;
        var position = transform.position;
        buffer.PutFloat(position.x);
        buffer.PutFloat(position.y);
        buffer.PutFloat(position.z);
        var rotation = transform.rotation;
        buffer.PutFloat(rotation.x);
        buffer.PutFloat(rotation.y);
        buffer.PutFloat(rotation.z);
        buffer.PutFloat(rotation.w);
    }
    
    public void UpdatePlayerInput(PlayerInput playerInput)
    {
        _playerInput = playerInput;
    }
}