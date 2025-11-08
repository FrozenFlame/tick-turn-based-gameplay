
using System.Collections.Generic;
using UnityEngine;
using BattleScripts.Enums;
using System.Collections.ObjectModel;

namespace BattleScripts
{
    public class BattleContext
    {
        private List<Character> initial_characters;
        public ReadOnlyCollection<Character> all_characters;
        public List<Character> player_characters;
        public List<Character> enemy_characters;

        public Character active_character;
        public Character last_clicked_character;

        public BattleContext()
        {
            initial_characters = new List<Character>();
            all_characters = initial_characters.AsReadOnly();
            player_characters = new List<Character>();
            enemy_characters = new List<Character>();
        }

        public void AddCharacterToContext(Character character)
        {
            initial_characters.Add(character);
        }

        public void SortRosters()
        {
            player_characters.Clear();
            enemy_characters.Clear();
            foreach (Character character in all_characters)
            {
                if (character.faction == CharacterFactionEnum.FRIENDLY) player_characters.Add(character);
                else if (character.faction == CharacterFactionEnum.ENEMY) enemy_characters.Add(character);
            }
        }

        public void ClearPointers()
        {
            active_character = null;
            last_clicked_character = null;
        }

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
            if (source.faction == CharacterFactionEnum.FRIENDLY)
            {
                return new List<Character>(player_characters);
            }
            else if (source.faction == CharacterFactionEnum.ENEMY)
            {
                return new List<Character>(enemy_characters);
            }

            return new List<Character>();
        }

        public List<Character> GetEnemiesOf(Character source)
        {
            if (source.faction == CharacterFactionEnum.FRIENDLY)
            {
                return new List<Character>(enemy_characters);
            }
            else if (source.faction == CharacterFactionEnum.ENEMY)
            {
                return new List<Character>(player_characters);
            }

            return new List<Character>();
        }

        public Character GetRandomAllyTarget(Character source)
        {
            if (source.faction == CharacterFactionEnum.FRIENDLY)
            {
                if (player_characters.Count == 0) return null;
                int index = Random.Range(0, player_characters.Count);
                return player_characters[index];
            }
            else if (source.faction == CharacterFactionEnum.ENEMY)
            {
                if (enemy_characters.Count == 0) return null;
                int index = Random.Range(0, enemy_characters.Count);
                return enemy_characters[index];
            }
            return null;
        }

        public Character GetRandomEnemyTarget(Character source)
        {
            if (source.faction == CharacterFactionEnum.FRIENDLY)
            {
                if (enemy_characters.Count == 0) return null;
                int index = Random.Range(0, enemy_characters.Count);
                return enemy_characters[index];
            }
            else if (source.faction == CharacterFactionEnum.ENEMY)
            {
                if (player_characters.Count == 0) return null;
                int index = Random.Range(0, player_characters.Count);
                return player_characters[index];
            }

            return null;
        }

        public List<Character> GetRandomTargets(int count, bool only_target_living = true)
        {
            if (all_characters.Count == 0) return new List<Character>();
            
            List<Character> potential_targets = new List<Character>(all_characters);

            if (only_target_living) potential_targets.RemoveAll(c => !c.IsAlive());

            int characters_ignored = Mathf.Max(0, potential_targets.Count - count);

            for (int i = 0; i < characters_ignored; i++)
            {
                int random_index = Random.Range(0, potential_targets.Count);
                potential_targets.RemoveAt(random_index);
            }

            return potential_targets;
        }
    }
}
