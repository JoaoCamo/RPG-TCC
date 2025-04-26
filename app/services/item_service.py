from openai import OpenAI

from app.utils.schema_util import load_json_schema

client = OpenAI()


def generate_item():
    item_system = "You are an imaginative Dungeon Master."
    item_user = "Generate this item based on the character."
    item_room_schema = load_json_schema("item_schema.json")
    response = client.responses.create(
        model="gpt-4.1-nano",
        input=[
            {"role": "system", "content": item_system},
            {"role": "user", "content": item_user},
        ],
        text=item_room_schema,
    )
    return response.output_text
