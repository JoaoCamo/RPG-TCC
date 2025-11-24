from flask import Blueprint, request, jsonify
from app.services.story_service import generate_story

story_blueprint = Blueprint("continue_story", __name__)


@story_blueprint.route("/", methods=["POST"])
def story():
    data = request.get_json()
    selected_model = data.get("selectedModel")
    choice = data.get("choice")
    story = generate_story(selected_model, choice)
    return jsonify(story)
