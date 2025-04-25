from openai import OpenAI

from app.utils.schema_util import load_json_schema

client = OpenAI()


def generate_dungeon():
    dungeon_system = "You are an imaginative Dungeon Master."
    dungeon_user = "Populate this dungeon room based on the dungeon theme."
    dungeon_schema = load_json_schema("dungeon_schema.json")
    response = client.responses.create(
        model="gpt-4.1-nano",
        input=[
            {"role": "system", "content": dungeon_system},
            {"role": "user", "content": dungeon_user},
        ],
        text=dungeon_schema,
    )
    return response.output_text
