using System;

namespace Models.Network
{
    public class EnvObjectModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CustomVector3 CustomVector { get; set; }
        public CustomQuaternion CustomQuaternion { get; set; }
        public CustomVector3 CustomScale { get; set; }
    }
}