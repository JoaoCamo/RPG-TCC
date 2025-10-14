import json
from openai import OpenAI

from app.utils.schema_util import load_json_schema
from app.utils.save_context import save_context, load_context

client = OpenAI()


def generate_story(choice):
    context = load_context("context.txt")
    story_system = "You are an imaginative Dungeon Master."
    story_user = f"Story Context: {context}\nPlayer choice: {choice}\nPrompt: Generate a story/response dialogue given by a named npc, based on the previous contexts of the story, then give the player three choices about what to say/do next. One of these choices must be about the story (game_state: 0), another one must be about the dungeon (game_state: 1), and another one an agressive option towards the npc. The npc might change depending on the players choice"
    story_schema = load_json_schema("story_schema.json")
    response = client.responses.create(
        model="gpt-4.1-nano",
        input=[
            {"role": "system", "content": story_system},
            {"role": "user", "content": story_user},
        ],
        text=story_schema,
    )
    main_story = json.loads(response.output_text)
    save_context(main_story, "context.txt")
    return main_story
