import json
from openai import OpenAI

from app.utils.schema_util import load_json_schema

client = OpenAI()

def generate_item(character: str, item_type: str):
    item_system = "You are an imaginative Dungeon Master."
    item_user = f"Generate a {item_type}, based on the character that will drop it, the item should be corresponding to the character: {character}"

    if item_type == "Weapon":
        item_schema = load_json_schema("weapon_schema.json")
    else:
        item_schema = load_json_schema("armor_schema.json")

    response = client.responses.create(
        model="gpt-4.1-nano",
        input=[
            {"role": "system", "content": item_system},
            {"role": "user", "content": item_user},
        ],
        text=item_schema,
    )
    return json.loads(response.output_text)