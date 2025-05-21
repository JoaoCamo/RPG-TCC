from flask import Blueprint, request, jsonify
from app.services.story_service import generate_story

story_blueprint = Blueprint("continue_story", __name__)


@story_blueprint.route("/", methods=["POST"])
def story():
    story = generate_story()
    return jsonify({"story": story})
