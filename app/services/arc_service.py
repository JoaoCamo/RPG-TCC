import json
from openai import OpenAI

from app.utils.schema_util import load_json_schema
from app.utils.context_helper import save_context, summarize_context

client = OpenAI()

def generate_arc(selected_model: str, last_action: str, current_arc: int, is_ending: bool):
    summarize_context(client)
    context = summarize_context(client)
    arc_system = "You are an imaginative worldbuilder Dungeon Master for a fantasy game."
    arc_user = f"""
    Story context so far: {context}.
    The player's last major action was: {last_action}.
    This begins Arc {current_arc} — unless this is the ending.

    The story must naturally progress in a way that leads to its final conclusion by Arc 5 at the latest.

    If isEnding = {is_ending}, this is the **final conclusion of the entire story**, not an arc:
        - Do NOT treat this as an arc and do NOT use arc-style titles.
        - Title format must be like an ending or epilogue (e.g., "Epilogue: The Last Dawn", "The Story's End", "Final Resolution").
        - Provide a complete, definitive ending that resolves all major plotlines.
        - Give emotional and narrative closure to all central characters.
        - No new conflicts, no new mysteries, no cliffhangers, no setups for future arcs.
        - The text should read like the last chapter of a book.
        - Do NOT include dialogue options or continue the narrative beyond this ending.

    If isEnding = {is_ending}:
        - This is a new story arc.
        - The title MUST include the arc number (e.g., "Arc {current_arc} – The Shadow Stirs").
        - The section should introduce the tone, stakes, and direction for this arc.
        - Ensure that the story’s progression feels meaningful and moves closer to its eventual ending by Arc 5.
        - The narrative must follow naturally from the player's last major action.

    Ensure the narrative always follows the established story context and respects the consequences of the player's choices.
    """

    arc_schema = load_json_schema("arc_schema.json")
    response = client.responses.create(
        model=selected_model,
        input=[
            {"role": "system", "content": arc_system},
            {"role": "user", "content": arc_user},
        ],
        text=arc_schema,
    )
    arc = json.loads(response.output_text)
    save_context(arc, "context.txt")
    return arc