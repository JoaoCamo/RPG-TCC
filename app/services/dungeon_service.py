from openai import OpenAI

from app.utils.schema_util import load_json_schema

client = OpenAI()


def generate_dungeon(layout: dict):
    dungeon_system = "You are an imaginative Dungeon Master."
    dungeon_user = f"Populate each dungeon room based on this layout : {layout}"
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
