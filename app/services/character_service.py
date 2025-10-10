import json

from openai import OpenAI

from app.utils.schema_util import load_json_schema

client = OpenAI()


def generate_character(level: int, context: str):
    character_system = "You are an imaginative Dungeon Master."
    character_user = f"Generate a creative character sheet for a level {level} character, based on {context}, the chracter has a 30% chance to not have any itemDrop, 40% chance to have 1 itemDrop, 20% chance to have 2 itemDrops and 10% chance to have 3 itemDrops."
    character_schema = load_json_schema("character_schema.json")
    response = client.responses.create(
        model="gpt-4.1-nano",
        input=[
            {"role": "system", "content": character_system},
            {"role": "user", "content": character_user},
        ],
        text=character_schema,
    )
    return json.loads(response.output_text)
