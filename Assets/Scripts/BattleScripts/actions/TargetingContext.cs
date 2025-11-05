
using System.Collections.Generic;

namespace BattleScripts.Actions
{
    public class TargetingContext
    {
        public List<Character> all_characters;
        public List<Character> player_characters;
        public List<Character> enemy_characters;

        public Character last_clicked_character;
        public Character GetSelectedTarget()
        {
            return last_clicked_character;
        }
        public List<Character> GetAdjacentCharacters(Character character)
        {
            throw new System.NotImplementedException();
        }

        public List<Character> GetAlliesOf(Character source)
        {
            throw new System.NotImplementedException();
        }

        public List<Character> GetEnemiesOf(Character source)
        {
            throw new System.NotImplementedException();
        }

        public Character GetRandomEnemyTarget(Character source, bool can_target_any)
        {
            throw new System.NotImplementedException();
        }

        public List<Character> GetRandomTargets(Character source, int count, bool can_target_any)
        {
            throw new System.NotImplementedException();
        }
    }
}
