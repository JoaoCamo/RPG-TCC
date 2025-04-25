import json
from openai import OpenAI

from app.utils.schema_util import load_json_schema
from app.utils.save_context import save_context

client = OpenAI()


def generate_story():
    story_system = "You are an imaginative Dungeon Master."
    story_user = "Generate a story/response dialogue given by a named npc/item, based on the previous contexts of the story, then give the player three choices about what to say/do next."
    story_schema = load_json_schema("story_schema.json")
    response = client.responses.create(
        model="gpt-4.1-mini",
        input=[
            {"role": "system", "content": story_system},
            {"role": "user", "content": story_user},
        ],
        text=story_schema,
    )
    main_story = json.loads(response.output_text)
    save_context(main_story)
    return response.output_text
