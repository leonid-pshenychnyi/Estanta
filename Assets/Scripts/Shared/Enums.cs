namespace Shared
{
    public enum CameraPositionType
    {
        FirstPersonView = 0,
        ThirdPersonView = 1,
        Free = 2
    }

    public enum ObjectPosition
    {
        Character = 0,
        Env = 1,
        Npc = 2
    }

    public enum Ports
    {
        ServerCharacters = 732,
        UserCharacters = 733,
    
        ServerEnvironment = 734,
        UserEnvironment = 735,
    
        ServerNpc = 736,
        UserNpc = 737,
    
        Users = 738
    }
}