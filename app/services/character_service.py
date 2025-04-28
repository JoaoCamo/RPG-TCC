from openai import OpenAI

from app.utils.schema_util import load_json_schema

client = OpenAI()


def generate_character(level: int):
    character_system = "You are an imaginative Dungeon Master."
    character_user = f"Generate a creative character sheet for a level {level} character, based on previous dungeon and kingdom contexts, the chracter has a 50% chance to not have any itemDrop, 25% chance to have 1 itemDrop, 12.5% chance to have 2 itemDrops and 12.5% chance to have 3 itemDrops."
    character_schema = load_json_schema("character_schema.json")
    response = client.responses.create(
        model="gpt-4.1-nano",
        input=[
            {"role": "system", "content": character_system},
            {"role": "user", "content": character_user},
        ],
        text=character_schema,
    )
    return response.output_text
