import json

from openai import OpenAI

from app.utils.schema_util import load_json_schema

client = OpenAI()


def generate_character(level: int, context: str):
    character_system = "You are an imaginative Dungeon Master."
    character_user = f"""
                        Generate a creative character sheet for a level {level} character based on the following context: {context}.

                        Ensure that all abilities, stats, and items are appropriate for the character's level.

                        Randomize the character's item drops using these probabilities:
                            - 30% chance of having no items
                            - 40% chance of having 1 item
                            - 20% chance of having 2 items
                            - 10% chance of having 3 items

                        Include the character's:
                            - Abilities and skills suitable for the level
                            - Potential loot and items
                        
                        The character sheet should be engaging, balanced, and suitable for a tabletop or RPG setting.
                      """
    character_schema = load_json_schema("character_schema.json")
    response = client.responses.create(
        model="gpt-4.1",
        input=[
            {"role": "system", "content": character_system},
            {"role": "user", "content": character_user},
        ],
        text=character_schema,
    )
    return json.loads(response.output_text)
