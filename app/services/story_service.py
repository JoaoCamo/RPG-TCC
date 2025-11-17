import json
from openai import OpenAI

from app.utils.schema_util import load_json_schema
from app.utils.context_helper import save_context, load_context

client = OpenAI()


def generate_story(selected_model: str, choice):
    context = load_context("context.txt")
    story_system = "You are an imaginative Dungeon Master."
    story_user = f"""
    Story Context: {context}
    Player choice: {choice}

    Prompt:
    Generate a story or dialogue response from a named NPC based on the previous story context. 
    The NPC should naturally change over time as the story progresses — not only when entering or leaving the dungeon. 
    NPC transitions should occur organically as conversations evolve, the player explores the world, or new situations unfold.

    All dialogue and choices must update and reflect the current game_state of the dungeon system.

    Provide the player with **at least 5 and at most 7** choices for what to say or do next.
    The choices should:
    - Include different approaches (game_state: 1) such as advancing the story, social interaction, exploration, or confrontation.
    - Include dungeon-related choices (game_state: 2), but these should become more frequent as the narrative progresses.
    - Gradually shift the story toward the dungeon: after enough interactions, the available options should eventually become **exclusively dungeon-related**.
    - Reflect changes in NPCs, tone, and situation based on the player’s previous decisions.

    Ensure the dialogue, pacing, NPC changes, and player choices feel natural, engaging, and coherent with the ongoing story context.
    """

    story_schema = load_json_schema("story_schema.json")
    response = client.responses.create(
        model=selected_model,
        input=[
            {"role": "system", "content": story_system},
            {"role": "user", "content": story_user},
        ],
        text=story_schema,
    )
    main_story = json.loads(response.output_text)
    save_context(main_story, "context.txt")
    return main_story
