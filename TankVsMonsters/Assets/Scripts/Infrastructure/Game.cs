using Services.Input;

namespace Infrastructure
{
    public class Game
    {
        public static IInputService InputService { get; private set; }

        public Game()
        {
            RegisterInputService();
        }

        private static void RegisterInputService()
        {
            var inputService = new GameInputService();
            inputService.Enable();
            InputService = inputService;
        }
    }
}