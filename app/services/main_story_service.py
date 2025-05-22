import json
from openai import OpenAI

from app.utils.schema_util import load_json_schema
from app.utils.save_context import save_context

client = OpenAI()


def generate_main_story(type: str, name: str):
    main_story_system = "You are an imaginative worldbuilder Dungeon Master for a fantasy game."
    main_story_user = f"Generate a creative but succinct introductory story about a level 1 {type} called {name} that enters a kingdom and wants to become a hero by defeating its dungeon. Define a name and a brief description for the kingdom be creative with the name and description specifying a theme and race for example: Dwarfs, Elfs, Halflings, Humans, Orcs, Tieflings, etc, then do the same for the dungeon specifying a theme and race for example: Kobolds, Goblins, Fiends, Constructs, Undead, Beasts, etc."
    main_story_schema = load_json_schema("main_story_schema.json")
    response = client.responses.create(
        model="gpt-4.1",
        input=[
            {"role": "system", "content": main_story_system},
            {"role": "user", "content": main_story_user},
        ],
        text=main_story_schema,
    )
    main_story = json.loads(response.output_text)
    save_context(main_story, "context.txt")
    return response.output_text
